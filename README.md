[![Build Status](https://dev.azure.com/bauchart/ShadowRoller/_apis/build/status/WouterBau.ShadowRoller?branchName=main)](https://dev.azure.com/bauchart/ShadowRoller/_build/latest?definitionId=2&branchName=main)
![Azure DevOps tests](https://img.shields.io/azure-devops/tests/bauchart/ShadowRoller/2/main?compact_message)
![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/bauchart/ShadowRoller/2)

# ShadowRoller
A small Discord bot currently set up as an easier way to resolve ShadownRun 5 tests.
It reads and sends messages in the channel they're invited to.
It calculates the hits and whether you (critically) glitched.

## Commands
Commands always start with `!sr-` to trigger the robot to answer and calculate.

### Roll amount dice `!sr-roll XdY... Z ...`
This will roll the specified X amount of dY dice collections and calculate a sum result based on it with the provided collection of Z modifiers.

Ex.: `!sr-roll 4d6 1d8 2 -1`

- `X` : Amount of dice to roll.
- `Y` : Amount sides of the requested die
- `Z` : Modifiers for the end result (optional)

### Test by abilities and skills `!sr-sr5 X Y ... [Z]`
This will roll an amount of 6d dice based on the collection of attribute values passed in and calculate the ShadowRun result based on the values and provided limit.

*In other words, this will sum up all values provided first.*

Ex.: `!sr-test 1 4 2 [3]`

- `X Y ...` : List of attribute and skill values to use in a test. It makes a sum of these values to use as the dice pool for the roll.
- `[Z]` : Limit of hits (Optional)