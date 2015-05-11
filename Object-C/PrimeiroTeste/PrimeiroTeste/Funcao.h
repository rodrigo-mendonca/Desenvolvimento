//
//  Funcao.h
//  PrimeiroTeste
//
//  Created by Rodrigo Mendonça on 23/03/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface Funcao : UIViewController{
    IBOutlet UITextField *N1,*N2,*N3;
    IBOutlet UIWebView *WebSite;
}

@property(nonatomic,retain) UITextField *N1,*N2,*N3;
@property(nonatomic,retain) UIWebView *WebSite;

-(IBAction)bSomar:(id)sender;


@end