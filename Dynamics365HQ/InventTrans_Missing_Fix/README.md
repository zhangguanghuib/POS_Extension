##This script is to fix salesline whose inventroy transaction are missing for unknown reason, the idea to increase sales qty by one and then reduce it by one to trigger the inventory transaction got created successfully.
This script verified works fine in customer's production.

This script may not be the most right way to fix the inventory transaction missing issue, but it provide a way to change the sales line quantity through x++ code:


#1. Class Name
![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/Dynamics365HQ/InventTrans_Missing_Fix/Images/1.png?raw=true "Optional title")
#2. Find Salesline whose inventory transaction are missing
![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/Dynamics365HQ/InventTrans_Missing_Fix/Images/2.png?raw=true "Optional title")
#3.Fix the issue for specific salesling through increasing its quantity and then reduce it by one
![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/Dynamics365HQ/InventTrans_Missing_Fix/Images/3.png?raw=true "Optional title")
#4.Previous version to fix a single sales order
![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/Dynamics365HQ/InventTrans_Missing_Fix/Images/4.png?raw=true "Optional title")
#5.Main method to call
![Alt text](https://github.com/zhangguanghuib/POS_Extension/blob/main/Dynamics365HQ/InventTrans_Missing_Fix/Images/5.png?raw=true "Optional title")
