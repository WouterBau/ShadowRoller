# ShadowRoller
A small Discord bot currently set up as an easier way to resolve ShadownRun 5 tests.
It reads and sends messages in the channel they're invited to.
It calculates the hits and whether you (critically) glitched.

## Commands
Commands always start with `!sr-` to trigger the robot to answer and calculate.

### Roll amount dice
`!sr-roll X [Y]`
Ex.: `!sr-roll 4 [2]`
- `X` : Amount of dice to roll
- `[Y]` : Limit of hits (Optional)

### Test by abilities and skills
`!sr-test X Y ... [Z]`
Ex.: `!sr-test 1 4 2`
- `X Y ...` : List of attribute and skill values to use in a test. It makes a sum of these values to use as the dice pool for the roll.
- `[Z]` : Limit of hits (Optional)