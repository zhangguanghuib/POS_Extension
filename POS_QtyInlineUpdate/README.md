# POS_Extension
##This solution is to mimic Cart Qty InLine Update on POS before OOB product support this

#1. Go to Screen Layout, and find the Transaction Screen and add custom control
![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/POS_QtyInlineUpdate/Images/POS_Screenlayout.png?raw=true "Optional title")
#2. Make sure the manifest value matches the value in custom control
![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/POS_QtyInlineUpdate/Images/Manifest.png?raw=true "Optional title")
#3.For the below UI, after QTY or Price input done, please touch any place outside the input box or click the 'tick' button
![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/POS_QtyInlineUpdate/Images/UI1_POS.png?raw=true "Optional title")
#4.The html of this UI as below:
![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/POS_QtyInlineUpdate/Images/UI1_Html.png?raw=true "Optional title")
#5.This UI, whenever QTY changed, POS will talk to retail server to trigger the change immediately
![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/POS_QtyInlineUpdate/Images/UI2_POS.png?raw=true "Optional title")
#5.The html of UI way 2 is as below:
![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/POS_QtyInlineUpdate/Images/UI2_Html.png?raw=true "Optional title")
