# ShadowRoller
A small Discord bot currently set up as an easier way to resolve ShadownRun 5 tests.
It reads and sends messages in the channel they're invited to.
It calculates the hits and whether you (critically) glitched.

## Commands
Commands always start with `!sr-` to trigger the robot to answer and calculate.

### Roll amount dice `!sr-roll X [Y]`
This will roll the specified amount of 6d dice and calculate the ShadowRun result based on the values and provided limit.

**Only one value may be provided and requires to be > 0**

Ex.: `!sr-roll 4 [2]`

- `X` : Amount of dice to roll.
- `[Y]` : Limit of hits (Optional)

### Test by abilities and skills `!sr-test X Y ... [Z]`
This will roll an amount of 6d dice based on the collection of attribute values passed in and calculate the ShadowRun result based on the values and provided limit.

*In other words, this will sum up all values provided first.*

Ex.: `!sr-test 1 4 2`

- `X Y ...` : List of attribute and skill values to use in a test. It makes a sum of these values to use as the dice pool for the roll.
- `[Z]` : Limit of hits (Optional)

### Evaluate  `!sr-eval X Y ... [Z]`
This will evaluate the list of values provided according to the ShadowRun rules and the provided limit.

**Values may only contain a value >=1 or <=6**

Ex.: `!sr-eval 5 6 2 [2]`

- `X Y ...` : List of dice roll values to evaluate to calculate the result.
- `[Z]` : Limit of hits (Optional)