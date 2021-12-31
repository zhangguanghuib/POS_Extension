
# Project Title

This feature is implemented for some special industry like medicine, alcohol, tobacco and chemistry.  
When client buy a large quantity of product, and if the quantity exceeds quantity limitation speficied in D365 Commerce back office, retail store cashier need manager approval to complete the transaction, otherwise the transaction can not complete.

# How to use this solution

1. Enable the feature:
   ![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/CWExtensions/Images/1-EnableFeature.png?raw=true "Optional title")

2. Set quantity limitation through default order settings, in this scenario its max quantity is 50:
   ![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/CWExtensions/Images/2-SetDefaultOrderSettings.png?raw=true "Optional title")
   
3. Create POS operation:
   ![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/CWExtensions/Images/3-CreatePOSOperations.png?raw=true "Optional title")
   
4. Set worker's POS permissions, and make sure its Allow X-Priting is disabled:
   ![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/CWExtensions/Images/4-SetPOSPermission.png?raw=true "Optional title")
   
5. Run 9999 job and make sure download session applied.
6. Logon POS with the worker who has no permission to sell large quantity products:
   ![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/CWExtensions/Images/5-LogInPOS.png?raw=true "Optional title")
   
7.Sell product with its quantity exceed the max quantity specified on the default order settings:
  Sell quantity 100 which exceeds the max quantity on default order settings 50.
  ![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/CWExtensions/Images/6-SellLargeQuantityOnPOS.png?raw=true "Optional title")
  
8.Messagebox prompt to show which cartline exceed quantity limitation, and need manager approval to proceed:
  ![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/CWExtensions/Images/7-MessageNeedManagerApproval.png?raw=true "Optional title")
  
9.Manager approval:
  ![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/CWExtensions/Images/8-ManagerApproval.png?raw=true "Optional title")
  
10. Complete transaction after manager approval it:
   ![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/CWExtensions/Images/9-CompleteTransaction.png?raw=true "Optional title")
