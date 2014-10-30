#import <iOmniataAPI/iOmniataAPI.h>
/**
 * Converts C style string to NSString
 */
#define GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]

/**
 * Call initialize with api_key, uid and debug
 */
extern void Initialize(const char* api_key, const char* uid, bool debug)
{
	[iOmniataAPI initializeWithApiKey:GetStringParam(api_key) UserId:GetStringParam(uid) AndDebug:debug];
}

/**
 * Call TrackRevenue with total and currency_code
 */
extern void TrackRevenue(const double total, const char* currency_code)
{
	[iOmniataAPI trackPurchaseEvent:total currency_code: GetStringParam(currency_code)];
}

/**
 * Call TrackEvent with type and parameters
 * Convert parameters to NSMutableDictionary
 */
extern void TrackEvent(const char* type,const char *parameters) {
    NSString *attris = GetStringParam(parameters);
    NSArray *attributesArray = [attris componentsSeparatedByString:@"\n"];
    NSMutableDictionary *paraDict = [[NSMutableDictionary alloc] init];
    for (int i=0; i < [attributesArray count]; i++) {
        NSString *keyValuePair = [attributesArray objectAtIndex:i];
        NSRange range = [keyValuePair rangeOfString:@"="];
        if (range.location != NSNotFound) {
            NSString *key = [keyValuePair substringToIndex:range.location];
            NSString *value = [keyValuePair substringFromIndex:range.location+1];
            [paraDict setObject:value forKey:key];
        }
    }
    [iOmniataAPI trackEvent: GetStringParam(type):paraDict];
}

/**
 * Call TrackLoad
 */

extern void TrackLoad()
{
    [iOmniataAPI trackLoadEvent];
}

/**
 * Call GetChannelMessage with channelID
 * Return NSString result
 */

extern char* GetChannelMessage(const int channelID){
    static NSString * result;
    [iOmniataAPI loadMessagesForChannel:channelID completionHandler:^(OMT_CHANNEL_STATUS cs){
        NSArray* omniValues = [iOmniataAPI getChannelMessages];
        NSString * result = [omniValues description];
//      NSLog(@"omniValues:%@",omniValues);
//        return result;
    }];
    return result;
}

/**
 * Call Log method with message
 * Log in the Xcode console as "Omniata: <message>"
 */
extern void Log(const char* message)
{
    NSLog(@"%@: %@", @"Omniata", GetStringParam(message));
}




