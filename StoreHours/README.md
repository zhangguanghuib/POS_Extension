# Store Hours Guidence

1. Create  a new POS operations
![image](https://user-images.githubusercontent.com/14832260/147532226-8f7f48b5-c9d1-4863-a8df-1301a0aa0cde.png)
2. Add this POS  operation to button grid, and run 1090 job
![image](https://user-images.githubusercontent.com/14832260/147532299-4fa82918-20e4-4cf7-9b71-843b39f76602.png)

	3. Include the folder into project:
	
	
	Update the request code with the new operation id
	
	
	4. Update the manifest file, this step is very important, otherwise it will show this operation is not supported
	
	
	5. Update extensions.json
	
	6. Update tsconfig.json
	
	
	8. Build the CRT and retail server ,Comment these code if you don't have RTS
	
	
	9. Run the SQL file under 
	
	
	10. Run the below script to insert initial data:
	
	select * from [ax].RETAILSTORETABLE as T where T.STORENUMBER = 'HOUSTON'
	
	select * from  [ext].[CONTOSORETAILSTOREHOURSTABLE]
	
	delete from [ext].[CONTOSORETAILSTOREHOURSTABLE]
	insert into
	[ext].[CONTOSORETAILSTOREHOURSTABLE]
	values
	(1, 1, 28800, 61200, 5637144592),
	(2, 2, 28800, 61200, 5637144592),
	(3, 3, 28800, 61200, 5637144592),
	(4, 4, 28800, 61200, 5637144592),
	(5, 5, 28800, 61200, 5637144592),
	(6, 6, 28800, 61200, 5637144592),
	(7, 7, 28800, 61200, 5637144592)
	
	11. Click the button you added, and the store hours view can be opened as below:
	
	
	12. Choose one line can  update it:
	
	
![image](https://user-images.githubusercontent.com/14832260/147532474-63a1d022-5f3a-4e9b-9e21-a43d6767baec.png)
