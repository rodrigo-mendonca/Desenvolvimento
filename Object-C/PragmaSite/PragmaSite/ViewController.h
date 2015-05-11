//
//  ViewController.h
//  PragmaSite
//
//  Created by Rodrigo Mendonça on 20/03/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface ViewController : UIViewController{
    IBOutlet UIWebView *oViewSite;
    IBOutlet UINavigationItem *oNavItem;
}

@property(nonatomic,retain) IBOutlet UIWebView *oViewSite;
@property(nonatomic,retain) IBOutlet UINavigationItem *oNavItem;

-(IBAction)NavBack:(id)sender;

@end
