/* Copyright 2013 Bliksem Labs. See the LICENSE file at the top-level directory of this distribution and at https://github.com/bliksemlabs/rrrr/. */

/* util.c : various utility functions */
#include "util.h"

/*#include <syslog.h>*/
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <limits.h>
#include <time.h>

void die(const char *msg) {
    fprintf (stderr, "%s\n", msg);
    //syslog (LOG_ERR, "%s\n", msg);
    exit (EXIT_FAILURE);
}

static char buf[32];

// buffer should always be at least 13 characters long, including terminating null
char *btimetext(rtime_t rt, char *buf) {
	char *day;
	uint32_t t, m, s, h;
	if (rt == UNREACHED) {
        strcpy_s(buf, 10, "   --   ");
        return buf;
    }
    if (rt >= RTIME_THREE_DAYS) {
        day = " +2D";
        rt -= RTIME_THREE_DAYS;
    } else if (rt >= RTIME_TWO_DAYS) {
        day = " +1D";
        rt -= RTIME_TWO_DAYS;
    } else if (rt >= RTIME_ONE_DAY) {
        day = "";
        rt -= RTIME_ONE_DAY;
    } else {
        day = " -1D";
    }
    t = RTIME_TO_SEC(rt);
    s = t % 60;
    m = t / 60;
    h = m / 60;
    m = m % 60;
    _snprintf_s(buf, 16, 16, "%02d:%02d:%02d%s", h, m, s, day);
    return buf;
}

char *timetext(rtime_t t) {
    return btimetext(t, buf);
}

//assumes little endian http://stackoverflow.com/a/3974138/778449
// size in bytes
void printBits(size_t const size, void const * const ptr) {
    unsigned char *b = (unsigned char*) ptr;
    unsigned char byte;
    int i, j;
    for (i=size-1;i>=0;i--) {
        for (j=7;j>=0;j--) {
            byte = b[i] & (1<<j);
            byte >>= j;
            printf("%u", byte);
        }
    }
    puts("");
}

/*
  Converts the given epoch time to in internal RRRR router time.
  If epochtime is within the first day of 1970 it is interpreted as seconds since midnight 
  on the current day. If epochtime is 0, the current time and date are used.
  The intermediate struct tm will be copied to the location pointed to by *stm, unless stm is null.
  The date should be range checked in the router, where we can see the validity of the tdata file.
*/
rtime_t epoch_to_rtime (time_t epochtime, struct tm *tm_out) {
    struct tm ltm;
	uint32_t seconds;
	rtime_t rtime;
    if (epochtime < SEC_IN_ONE_DAY) {
        time_t now;
        time(&now);
        localtime_r (&now, &ltm); // LOCAL date and time
        if (epochtime > 0) {
            /* use current date but specified time */
            struct tm etm;
            gmtime_r (&epochtime, &etm); // UTC so timezone doesn't affect interpretation of time
            ltm.tm_hour = etm.tm_hour;
            ltm.tm_min  = etm.tm_min;
            ltm.tm_sec  = etm.tm_sec;
        }
    } else {
        /* use specified time and date */
        localtime_r (&epochtime, &ltm);
    }
    if (tm_out != NULL) {
        *tm_out = ltm;
    }
    seconds = (((ltm.tm_hour * 60) + ltm.tm_min) * 60) + ltm.tm_sec;
    rtime = SEC_TO_RTIME(seconds);
    /* shift rtime to day 1. day 0 is yesterday. */
    rtime += RTIME_ONE_DAY;
    /*
    printf ("epoch time is %ld \n", epochtime);
    printf ("epoch time is %s", ctime(&epochtime)); // ctime and asctime include newlines
    printf ("ltm is %s", asctime(&ltm));    
    printf ("seconds is %d \n", seconds);
    printf ("rtime is %d \n", rtime);
    */
    return rtime;
}
