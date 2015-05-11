//
//  AppDelegate_iPhone.h
//  Pragma
//
//  Created by Thiago Takehana on 13/02/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface AppDelegate_iPhone : NSObject <UIApplicationDelegate> {
    UIWindow *window;
	IBOutlet UIWebView *webView;
}

@property (nonatomic, retain) IBOutlet UIWindow *window;

@property (nonatomic, retain) UIWebView *webView;


-(IBAction)btnCloseApplication:(id)sender;

@end

