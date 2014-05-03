/* Copyright 2013 Bliksem Labs. See the LICENSE file at the top-level directory of this distribution and at https://github.com/bliksemlabs/rrrr/. */

/* config.h */
#define _ARM_

#define RRRR_TEST_CONCURRENCY 4
#define RRRR_INPUT_FILE "timetable.dat"

// runtime increases roughly linearly with this value, though with target pruning it no longer seems to have as much effect
// this must be set to at least 2, because we re-use one array for the initial state
#define RRRR_MAX_ROUNDS 6

/* note that these values can cause missed transfers until we have guaranteed / timed transfers */

// specify in seconds
#define RRRR_WALK_SLACK_SEC  0
// specify in internal 4-second intervals!
#define RRRR_XFER_SLACK_4SEC 0

// TODO: Max transfer time to avoid unnecessary branching?

// bind does not work with names (localhost) but does work with * (all interfaces)
#define CLIENT_ENDPOINT "tcp://127.0.0.1:9292"
#define WORKER_ENDPOINT "tcp://127.0.0.1:9293"

// use named pipes instead
// #define CLIENT_ENDPOINT "ipc://client_pipe"
// #define WORKER_ENDPOINT "ipc://worker_pipe"

// #define RRRR_INFO
// #define RRRR_DEBUG // do not name this DEBUG because some IDEs may define DEBUG
// #define RRRR_TRACE

/* http://stackoverflow.com/questions/1644868/c-define-macro-for-debug-printing */
#ifdef RRRR_INFO
 #define I 
#else
 #define I for(;0;)
#endif

#ifdef RRRR_DEBUG
 #define D 
#else
 #define D for(;0;)
#endif

#ifdef RRRR_TRACE
 #define T 
#else
 #define T for(;0;)
#endif

#define OUTPUT_LEN 8000


