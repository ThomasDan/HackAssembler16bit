
// First we set D to be @SCREEN's starting position in the RAM
@SCREEN
D=A

// Here we save that position to @R0
@R0
M=D

(LOOP)
// @R0's position is set to its Value..
@R0
A=M
// ..whereafter the value at the new position is set to -1 (meaning all 16 bits should be painted in)
M=-1

// @R0's Value++
@R0
M=M+1

// Check if value has exceeded the screen limit
@24577
D=A
@R0
D=D-M
@END
D;JEQ

// Repeat
@LOOP
0;JMP

(END)
@END
0;JMP


