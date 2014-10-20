#import "LoggerHelper.h"
// Converts C style string to NSString
#define GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]
extern void _log(const char* message)
{
 	LoggerHelper* helper = [[LoggerHelper alloc]init];
    [helper log:GetStringParam(message)];
}