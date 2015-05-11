//
//  Funcao.m
//  PrimeiroTeste
//
//  Created by Rodrigo Mendonça on 23/03/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//
#import "Funcao.h"

@implementation Funcao
@synthesize N1,N2,N3,WebSite;

-(IBAction)bSomar:(id)sender{
    double nSoma = 0;
    
    
    NSString *cN1 = N1.text;
    NSString *cN2 = N2.text;
    
    nSoma = cN1.doubleValue + cN2.doubleValue;
    
    N3.text = [NSString stringWithFormat:@"%f",nSoma];
    
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    
    // Release any cached data, images, etc that aren't in use.
}

#pragma mark - View lifecycle

- (void)viewDidLoad
{
    [super viewDidLoad];
    
    NSString *cURL = @"https://www.pragmapatrimonio.com.br/Sistema/Sistema/Login.aspx";
    
    NSURL *Url = [NSURL URLWithString:cURL];
    
    NSURLRequest *URLR = [NSURLRequest requestWithURL:Url];
    
    [WebSite loadRequest:URLR];
    
    // Do any additional setup after loading the view, typically from a nib.
}

- (void)viewDidUnload
{
    self.N1 = nil;
    [super viewDidUnload];
    // Release any retained subviews of the main view.
    // e.g. self.myOutlet = nil;
}

- (void)viewWillAppear:(BOOL)animated
{
    [super viewWillAppear:animated];
}

- (void)viewDidAppear:(BOOL)animated
{
    [super viewDidAppear:animated];
}

- (void)viewWillDisappear:(BOOL)animated
{
	[super viewWillDisappear:animated];
}

- (void)viewDidDisappear:(BOOL)animated
{
	[super viewDidDisappear:animated];
}

- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation
{
    // Return YES for supported orientations
    if ([[UIDevice currentDevice] userInterfaceIdiom] == UIUserInterfaceIdiomPhone) {
        return (interfaceOrientation != UIInterfaceOrientationPortraitUpsideDown);
    } else {
        return YES;
    }
}

@end