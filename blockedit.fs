\ blockedit.fs
only forth definitions
vocabulary editor
only forth also editor definitions
variable r#
create ibuf $100 allot
create fbuf $100 allot
: rvson  $1b emit ." [7m" ;
: rvsoff  $1b emit ." [0m" ;
: line# ( - n) r# @ 64 / ;
: char# ( - n) r# @ 64 mod ;
: rest ( - n)  64 char# - ;
: hi  char# 3 + line# 1+ at-xy
    rvson scr @ block r# @ +  rest type
    rvsoff 0 &17 at-xy ;
: l  page scr @ list editor hi .s ;
: n  1 scr +! r# off ;
: b  -1 scr @ + 0 max scr ! r# off ;
: wipe  scr @ block 1024 blank r# off l ;
: insert ( string length buffer size)
    rot over min >r  r@ -  over dup r@ +
    rot move  r> move ;
: delete ( buffer size count)
    over min >r  r@ -  dup 0> if
        2dup swap dup r@ +   -rot swap move
    then + r> blank ;
: 'rest ( - a u)  scr @ block 1024  r# @ /string ;
: 'line ( - a u)  'rest 1- 63 and 1+ ;
: 'parse ( buf - a u)  >r 0 parse dup
    0= if 2drop r> count else 2dup r> place then ;
: home  r# @ dup 63 and - r# ! l ;
: end  home 'rest drop 64 -trailing r# +! drop l ;
: t ( n - )  0 max 15 min 64 * r# ! l ;
: >buf ( a - )  >r 0 parse dup if 2dup r@ place then
    2drop r> drop ;
: >ibuf  ibuf >buf ;
: >fbuf  fbuf >buf ;
: p  line# t  r# @ scr @ block r# @ + 64 blank
    >ibuf ibuf count dup if
        2dup 'line insert update then
     2drop r# ! update l ;
: u  home 'rest 64 - over 64 + over insert
    line# 1+ t p ;
: i  ibuf 'parse 'line insert update l ;
: (f) ( - flag)  >fbuf 'rest 1 /string fbuf count search if
        nip 1024 swap - r# ! l false exit
     then  2drop l true ;
: f   (f) if ." none " then ;
: e  'rest drop rest fbuf c@ delete update l ;
: d  f e ;
: r  e i ;
: x  home 'line ibuf place  'rest 64 delete update l ;
: k  ibuf count pad place  fbuf count ibuf place
    pad count fbuf place ;
: till  r# @ >r  'rest drop rest 
    f r# @ fbuf c@ + r@ - delete  r> r# ! l ;
2variable 'other 0 0 'other 2!
: other ( n)  0 'other 2! ;
: o  scr @ r# @ 'other 2@ r# ! scr ! 'other 2! l ;
: s ( n - /n)  >r
    begin (f) while
        scr @ r@ = if r> drop ." none " exit then
        scr @ r@ u< if n else b then l
    repeat r> l ;
: lx  page  scr @ 60 / 60 * 60 bounds
    do 3 0 do [ forth ] i [ editor ] j + dup 3 .r
        space dup scr @ = if rvson then block 20 type rvsoff
    loop cr 3 +loop  ;
: nx  60 scr +! lx ;
: bx  scr @ 60 - 0 max scr ! lx ;
: g ( block line)  home r# @ -64 + 0 max >r
    swap other o 64 * r# ! x  o r> r# ! u ;
: copy ( n - )  other o 0 r# ! 'rest
    o 'rest drop swap move update l ;
also forth definitions
: l  l ;
: edit ( n - )  scr ! l ;
only forth also forth definitions

