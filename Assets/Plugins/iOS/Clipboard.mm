//
//  Clipboard.mm
//  Unity-iPhone
//
//  Created by hidemasa_mori on 2014/10/31.
//
//
NSString* CB_CreateNSString(const char* string)
{
    return [NSString stringWithUTF8String: string ? string : ""];
}

char* CB_MakeStringCopy(const char* string)
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
        NSString *nsstringText = CB_CreateNSString(text);
        UIPasteboard *pasteboard = [UIPasteboard generalPasteboard];
        pasteboard.string = nsstringText;
        return CB_MakeStringCopy([pasteboard.name UTF8String]);
    }
    
    char *_SetImage(const char* textureURL)
    {
        NSString *_textureURL = CB_CreateNSString(textureURL);
        if ([_textureURL length] != 0) {
            UIImage *image = [UIImage imageWithContentsOfFile:_textureURL];
            if (image == nil) {
                return CB_MakeStringCopy([@"" UTF8String]);
            }
            
            UIPasteboard *pasteboard = [UIPasteboard generalPasteboard];
            pasteboard.image = image;
            return CB_MakeStringCopy([pasteboard.name UTF8String]);
        } else {
            return CB_MakeStringCopy([@"" UTF8String]);
        }
    }
    
    
    char *_GetClipboard()
    {
        UIPasteboard *pasteboard = [UIPasteboard generalPasteboard];
        NSString *string = [pasteboard valueForPasteboardType:@"public.text"];
        return CB_MakeStringCopy([string UTF8String]);
    }
}