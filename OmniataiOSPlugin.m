#import <iOmniataAPI/iOmniataAPI.h>
// Converts C style string to NSString
#define GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]

extern void Initialize(const char* api_key, const char* user_id, bool debug)
{
	[iOmniataAPI initializeWithApiKey:GetStringParam(api_key) UserId:GetStringParam(user_id) AndDebug:debug];
}

extern void TrackRevenue(const double total, const char* currency_code)
{
	[iOmniataAPI trackPurchaseEvent:total currency_code: GetStringParam(currency_code)];
}

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

extern void TrackLoad()
{
    [iOmniataAPI trackLoadEvent];
}

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

extern void Log(const char* message)
{
    NSLog(@"%@: %@", @"Omniata", GetStringParam(message));
}




