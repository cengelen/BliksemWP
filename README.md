BliksemWP
=========

Port of Bliksem RRRR routing engine to Windows Phone with a planning frontend

Solution consists of three components:

1. **BliksemWP** - The Windows Phone frontend. Does stop search, and then calls the backend routing engine, and renders its results. 
1. **SQLiteWinRTPhone** - SQLite version for Windows Phone, from http://sqlwinrt.codeplex.com/ . This version is 'special' - it has support for FTS4 full text search that is used to render stops.
1. **NcxPppp** - RRRR as a library was LibRrrr, but during late night coding sessions during 24barcode certain people wanted a Dvorak keyboard layout, leading to this spelling. The RRRR code (https://github.com/bliksemlabs/rrrr) has been ported to C89 and Windows RT. Lots of changes for C89 and some Windows API hacking for memory mapped files (inspired by the SQLite above). 