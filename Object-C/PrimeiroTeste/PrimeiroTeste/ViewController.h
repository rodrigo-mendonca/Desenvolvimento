//
//  ViewController.h
//  PrimeiroTeste
//
//  Created by Rodrigo Mendonça on 30/12/11.
//  Copyright (c) 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface ViewController : UIViewController{
    IBOutlet UITextField *N1,*N2,*N3;
    IBOutlet UIWebView *WebSite;
}

@property(nonatomic,retain) UITextField *N1,*N2,*N3;
@property(nonatomic,retain) UIWebView *WebSite;

-(IBAction)bSomar:(id)sender;


@end
