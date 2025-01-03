A block editor based on the one described in Starting Forth. It may seem weird that the code is in a file that I edited with vim, but I needed a starting point. Now I can run gforth and include blockedit.fs to then edit Forth in blocks. The documentation is in Starting Forth. The online version omits the editor, but the PDF available at Forth Inc. still has it.

blocks.fb has the editor in the beginning of the file and go now uses that file for blocks and loads the editor after starting gforth. The text file blockedit.fs got me started, then I used it to make blocks.fb. Blocks.fb has diverged from blocksedit.fs and I see no reason to keep them in sync now. I will probably keep updating blocks.fb as I find that I want new commands to make my life easier.

I added ^, v, >, and <, meaning up, down, right, and left, for single character movement. Sometimes it's just easier than using t.
