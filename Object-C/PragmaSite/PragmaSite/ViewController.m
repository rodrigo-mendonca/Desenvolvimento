//
//  ViewController.m
//  PragmaSite
//
//  Created by Rodrigo Mendonça on 20/03/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "ViewController.h"

@interface ViewController ()

@end

@implementation ViewController;
@synthesize oViewSite,oNavItem;

- (void)viewDidLoad
{
    [super viewDidLoad];
    [oViewSite loadRequest:[NSURLRequest requestWithURL:[NSURL URLWithString:@"https://www.pragmapatrimonio.com.br/Sistema/Sistema/Login.aspx"]]];
    
    
	// Do any additional setup after loading the view, typically from a nib.
}

- (void)viewDidUnload
{
    [super viewDidUnload];
    // Release any retained subviews of the main view.
}

- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation
{
    if ([[UIDevice currentDevice] userInterfaceIdiom] == UIUserInterfaceIdiomPhone) {
        return (interfaceOrientation != UIInterfaceOrientationPortraitUpsideDown);
    } else {
        return YES;
    }
}

-(IBAction)NavBack:(id)sender{
    [oViewSite goBack];
}

@end
