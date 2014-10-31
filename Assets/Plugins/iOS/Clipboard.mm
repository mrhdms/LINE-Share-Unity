//
//  Clipboard.mm
//  Unity-iPhone
//
//  Created by hidemasa_mori on 2014/10/31.
//
//
NSString* CreateNSString(const char* string)
{
    return [NSString stringWithUTF8String: string ? string : ""];
}

char* MakeStringCopy(const char* string)
{
    if (string == NULL) return NULL;
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

extern "C"
{
    char *_SetText(const char* text)
    {
        NSString *nsstringText = CreateNSString(text);
        UIPasteboard *pasteboard = [UIPasteboard generalPasteboard];
        pasteboard.string = nsstringText;
        return MakeStringCopy([pasteboard.name UTF8String]);
    }
    
    char *_SetImage(const char* textureURL)
    {
        NSString *_textureURL = CreateNSString(textureURL);
        if ([_textureURL length] != 0) {
            UIImage *image = [UIImage imageWithContentsOfFile:_textureURL];
            if (image == nil) {
                return MakeStringCopy([@"" UTF8String]);
            }
            
            UIPasteboard *pasteboard = [UIPasteboard generalPasteboard];
            pasteboard.image = image;
            return MakeStringCopy([pasteboard.name UTF8String]);
        } else {
            return MakeStringCopy([@"" UTF8String]);
        }
    }
    
    
    char *_GetClipboard()
    {
        UIPasteboard *pasteboard = [UIPasteboard generalPasteboard];
        NSString *string = [pasteboard valueForPasteboardType:@"public.text"];
        return MakeStringCopy([string UTF8String]);
    }
}