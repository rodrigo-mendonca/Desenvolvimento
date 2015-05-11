//
//  ViewController.h
//  Interface
//
//  Created by Rodrigo Mendonça on 13/02/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface ViewController : UIViewController{
    IBOutlet UITextView *TextTela;
}
@property(nonatomic,retain) UITextView *TextTela;

- (IBAction)ClickBotao:(id)sender;

@end
