// R0 is the biggest number
// R1 is the smaller number
// R2 is the result increment
// R3 is the accumulation of potential increment
// R4 is the potential result increment (i henceforth)

@R2
M=0
@R3
M=0
@R4
M=0

(LOOP)

// Increasing Potential Result by Smallest number
@R1
D=M
@R3
M=M+D

// i++
@R4
M=M+1


// Testing if potential Result has exceeded biggest number
@R0
D=M
@R3
D=D-M

@END // Less than 0, we keep result as it is.
D;JLT
@EXACT // Exactly 0
D;JEQ

// Saving the Potential Result to Result
@R4
D=M
@R2
M=D

@LOOP
0;JMP

(EXACT)
@R4
D=M
@R2
M=D

(END)
@END
0;JMP