#!/bin/bash -e

# usage: destroy.sh <stack-name|stack-arn>
# default value for stack-name is get from the file stack_arn.txt

C="\033[1;32m"
R="\033[0m"

# usage function
usage() {
    echo -e "usage: destroy.sh <stack-arn>"
    echo -e "  defaults: stack-name=\`cat stack_arn.txt\`"
}

# check for correct number of arguments
if [ $# -gt 1 ]; then
    usage
    exit 1
fi

if [ "$1" == "--help" ] || [ "$1" == "-h" ]; then
    usage
    exit 0
fi

STACK_ARN=${1:-$(cat stack_arn.txt)}

# if empty, abort
if [ -z "$STACK_ARN" ]; then
    echo -e "No stack name or ARN provided (or found in stack_arn.txt)"
    usage
    exit 1
fi

echo -e "This will delete the stack $C${STACK_ARN}$R in AWS."
read -p "Are you sure? (y/N) " -r
echo
if [[ ! $REPLY =~ ^[Yy]$ ]]
then
    exit 1
fi


spinner()
{
    local pid=$1
    local delay=0.75
    local spinstr='|/-\'
    while [ "$(ps a | awk '{print $1}' | grep $pid)" ]; do
        local temp=${spinstr#?}
        printf " [%c]  " "$spinstr"
        local spinstr=$temp${spinstr%"$temp"}
        sleep $delay
        printf "\b\b\b\b\b\b"
    done
    printf "    \b\b\b\b"
}

# check if stack exists
STACK_QUERY=$(aws cloudformation list-stacks --stack-status-filter CREATE_COMPLETE UPDATE_COMPLETE ROLLBACK_COMPLETE --query "StackSummaries[?StackId=='$STACK_ARN']" --output json)
# check if query is empty

if [ -z "$STACK_QUERY" ]; then
    echo -e "Stack $C$STACK_ARN$R does not exist"
    exit 1
fi

# check if stack is in a valid state
STACK_STATUS=$(echo $STACK_QUERY | jq -r '.[0].StackStatus')
if [ "$STACK_STATUS" != "CREATE_COMPLETE" ] && [ "$STACK_STATUS" != "UPDATE_COMPLETE" ] && [ "$STACK_STATUS" != "ROLLBACK_COMPLETE" ]; then
    echo -e "Stack $C$STACK_ARN$R is in state $C$STACK_STATUS$R, cannot delete"
    exit 1
fi

# delete stack
echo -e "Deleting stack $C$STACK_ARN$R"
aws cloudformation delete-stack --stack-name "$STACK_ARN"

# wait for stack to be deleted
echo -e -n "Waiting for stack to be deleted"
aws cloudformation wait stack-delete-complete --stack-name "$STACK_ARN" & spinner $!
echo -e

rm stack_arn.txt
echo -e "Stack deleted (and stack_arn.txt removed)"

echo -e