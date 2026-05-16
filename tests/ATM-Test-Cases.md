# ATM System - Test Cases (Full Suite)

---

## TC001 - Register
- Feature: Register  
- Scenario: Successful registration  
- Preconditions: System is running  
- Steps: Open system > Register > Enter details > Submit  
- Input: Name: Ahmed / PIN: 1234 / Account Number: ACC001  
- Expected: Account created successfully  
- Actual: Account Created Successfully  
- Status: Pass  

---

## TC002 - Register Invalid PIN
- Feature: Register  
- Scenario: Invalid PIN (too short)  
- Preconditions: System is running  
- Steps: Register and enter invalid PIN  
- Input: PIN: 123  
- Expected: PIN must be exactly 4 digits  
- Actual: PIN must be exactly 4 digits  
- Status: Pass  

---

## TC003 - Register Alphanumeric PIN
- Feature: Register  
- Scenario: PIN contains letters  
- Preconditions: System is running  
- Steps: Enter alphanumeric PIN  
- Input: PIN: 12AB  
- Expected: PIN must be exactly 4 digits  
- Actual: PIN must be exactly 4 digits  
- Status: Pass  

---

## TC004 - Invalid Account Number Format
- Feature: Register  
- Scenario: Wrong account format  
- Preconditions: System is running  
- Steps: Enter invalid account number  
- Input: 1001  
- Expected: Must start with ACC and be ≥ 6 chars  
- Actual: Same validation message  
- Status: Pass  

---

## TC005 - Duplicate Account
- Feature: Register  
- Scenario: Existing account  
- Preconditions: ACC001 already exists  
- Steps: Register same account  
- Input: ACC001  
- Expected: Account already exists  
- Actual: Account Number Already Exists  
- Status: Pass  

---

## TC006 - Login Success
- Feature: Login  
- Scenario: Correct credentials  
- Preconditions: Account exists  
- Steps: Login with valid data  
- Input: ACC001 / 1234  
- Expected: Login successful  
- Actual: Login Successful  
- Status: Pass  

---

## TC007 - Login Invalid Account
- Feature: Login  
- Scenario: Non-existing account  
- Preconditions: System running  
- Steps: Login attempt  
- Input: ACC999 / 1234  
- Expected: Invalid account  
- Actual: Account not found  
- Status: Pass  

---

## TC008 - Login Wrong PIN Attempt 1
- Feature: Login  
- Scenario: Wrong PIN  
- Preconditions: Account exists  
- Steps: Enter wrong PIN  
- Input: ACC001 / 9999  
- Expected: Attempt count increases  
- Actual: Invalid PIN Attempt 1 of 3  
- Status: Pass  

---

## TC009 - Login Wrong PIN Lock
- Feature: Login  
- Scenario: 3 wrong attempts  
- Preconditions: Account exists  
- Steps: Enter wrong PIN 3 times  
- Input: ACC001 / 9999  
- Expected: Account locked  
- Actual: Account locked after 3 attempts  
- Status: Pass  

---

## TC010 - Login Locked Account
- Feature: Login  
- Scenario: Locked account  
- Preconditions: Account locked  
- Steps: Try login  
- Input: ACC001 / 1234  
- Expected: Account locked message  
- Actual: Account locked  
- Status: Pass  

---

## TC011 - Login Reset Attempts
- Feature: Login  
- Scenario: Successful login resets counter  
- Preconditions: Account unlocked  
- Steps: Login correctly  
- Input: ACC001 / 1234  
- Expected: Reset failed attempts  
- Actual: Login Successful  
- Status: Pass  

---

## TC012 - Deposit Valid
- Feature: Deposit  
- Scenario: Valid deposit  
- Preconditions: User logged in  
- Steps: Deposit money  
- Input: 500  
- Expected: Balance increases  
- Actual: Deposit Successful  
- Status: Pass  

---

## TC013 - Deposit Zero
- Feature: Deposit  
- Scenario: Zero amount  
- Preconditions: User logged in  
- Steps: Deposit 0  
- Input: 0  
- Expected: Invalid deposit  
- Actual: Invalid Deposit Amount  
- Status: Pass  

---

## TC014 - Deposit Negative
- Feature: Deposit  
- Scenario: Negative amount  
- Preconditions: User logged in  
- Steps: Deposit negative  
- Input: -100  
- Expected: Invalid deposit  
- Actual: Invalid Deposit Amount  
- Status: Pass  

---

## TC015 - Deposit Text
- Feature: Deposit  
- Scenario: Non-numeric input  
- Preconditions: User logged in  
- Steps: Enter text  
- Input: abc  
- Expected: Invalid input  
- Actual: Invalid amount  
- Status: Pass  

---

## TC016 - Withdraw Valid
- Feature: Withdraw  
- Scenario: Valid withdrawal  
- Preconditions: Sufficient balance  
- Steps: Withdraw money  
- Input: 100  
- Expected: Success  
- Actual: Withdraw Successful  
- Status: Pass  

---

## TC017 - Withdraw Insufficient Balance
- Feature: Withdraw  
- Scenario: Not enough balance  
- Preconditions: Low balance  
- Steps: Withdraw large amount  
- Input: 100000  
- Expected: Insufficient balance  
- Actual: Insufficient Balance  
- Status: Pass  

---

## TC018 - Withdraw Negative
- Feature: Withdraw  
- Scenario: Negative amount  
- Preconditions: User logged in  
- Steps: Withdraw negative  
- Input: -50  
- Expected: Invalid amount  
- Actual: Invalid Withdraw Amount  
- Status: Pass  

---

## TC019 - Transfer Valid
- Feature: Transfer  
- Scenario: Valid transfer  
- Preconditions: Accounts exist  
- Steps: Transfer money  
- Input: ACC002 / 200  
- Expected: Success transfer  
- Actual: Transfer Successful  
- Status: Pass  

---

## TC020 - Transfer Same Account
- Feature: Transfer  
- Scenario: Self transfer  
- Preconditions: User logged in  
- Steps: Transfer to same account  
- Input: ACC001  
- Expected: Cannot transfer  
- Actual: You cannot transfer to same account  
- Status: Pass  

---

## TC021 - Transfer Invalid Account
- Feature: Transfer  
- Scenario: Wrong account  
- Preconditions: User logged in  
- Steps: Transfer  
- Input: ACC999  
- Expected: Not found  
- Actual: Destination account not found  
- Status: Pass  

---

## TC022 - Transfer Insufficient Balance
- Feature: Transfer  
- Scenario: Large transfer  
- Preconditions: Low balance  
- Steps: Transfer money  
- Input: 999999  
- Expected: Insufficient balance  
- Actual: Insufficient Balance  
- Status: Pass  

---

## TC023 - Transaction History With Data
- Feature: History  
- Scenario: View transactions  
- Preconditions: Transactions exist  
- Steps: Open history  
- Input: N/A  
- Expected: List displayed  
- Actual: Same as expected  
- Status: Pass  

---

## TC024 - Empty Transaction History
- Feature: History  
- Scenario: No transactions  
- Preconditions: No data  
- Steps: Open history  
- Input: N/A  
- Expected: Empty list  
- Actual: Same as expected  
- Status: Pass  

---

## TC025 - Edit Name
- Feature: Edit Profile  
- Scenario: Change name  
- Preconditions: User logged in  
- Steps: Update name  
- Input: Ali  
- Expected: Updated  
- Actual: Name Updated Successfully  
- Status: Pass  

---

## TC026 - Change PIN
- Feature: Edit Profile  
- Scenario: Valid PIN update  
- Preconditions: User logged in  
- Steps: Change PIN  
- Input: 5678  
- Expected: Updated  
- Actual: PIN Updated Successfully  
- Status: Pass  

---

## TC027 - Invalid PIN Update
- Feature: Edit Profile  
- Scenario: Wrong format PIN  
- Preconditions: User logged in  
- Steps: Change PIN  
- Input: 12A  
- Expected: Error  
- Actual: PIN must be exactly 4 digits  
- Status: Pass  

---

## TC028 - Delete Account Confirm
- Feature: Delete  
- Scenario: Confirm delete  
- Preconditions: User logged in  
- Steps: Type yes  
- Input: yes  
- Expected: Deleted  
- Actual: Account deleted successfully  
- Status: Pass  

---

## TC029 - Delete Account Cancel
- Feature: Delete  
- Scenario: Cancel delete  
- Preconditions: User logged in  
- Steps: Type no  
- Input: no  
- Expected: Canceled  
- Actual: Account deletion canceled  
- Status: Pass  

---

## TC030 - Check Balance
- Feature: Balance  
- Scenario: View balance  
- Preconditions: Logged in  
- Steps: Check balance  
- Input: N/A  
- Expected: Show balance  
- Actual: Same as expected  
- Status: Pass  

---

## TC031 - Admin View Users
- Feature: Admin  
- Scenario: View users  
- Preconditions: Admin logged in  
- Steps: Option 1  
- Input: N/A  
- Expected: Users list  
- Actual: Same as expected  
- Status: Pass  

---

## TC032 - Admin View Accounts
- Feature: Admin  
- Scenario: View accounts  
- Preconditions: Admin logged in  
- Steps: Option 2  
- Input: N/A  
- Expected: Accounts list  
- Actual: Same as expected  
- Status: Pass  

---

## TC033 - Admin View Transactions
- Feature: Admin  
- Scenario: View transactions  
- Preconditions: Admin logged in  
- Steps: Option 3  
- Input: N/A  
- Expected: Transactions list  
- Actual: Same as expected  
- Status: Pass  

---

## TC034 - Admin Deposit
- Feature: Admin  
- Scenario: Deposit to account  
- Preconditions: Admin logged in  
- Steps: Select account  
- Input: ACC001 / 300  
- Expected: Balance increased  
- Actual: Deposit Successful  
- Status: Pass  

---

## TC035 - Admin Withdraw
- Feature: Admin  
- Scenario: Withdraw from account  
- Preconditions: Admin logged in  
- Steps: Select account  
- Input: ACC001 / 100  
- Expected: Balance decreased  
- Actual: Withdraw Successful  
- Status: Pass  

---

## TC036 - Admin Delete Account
- Feature: Admin  
- Scenario: Delete account  
- Preconditions: Admin logged in  
- Steps: Delete account  
- Input: ACC001  
- Expected: Account deleted  
- Actual: Account Deleted Successfully  
- Status: Pass  

---

## TC037 - Admin Edit User
- Feature: Admin  
- Scenario: Edit user  
- Preconditions: Admin logged in  
- Steps: Update user  
- Input: Name/PIN  
- Expected: Updated  
- Actual: User Updated Successfully  
- Status: Pass  

---

## TC038 - Admin Search Account
- Feature: Admin  
- Scenario: Search account  
- Preconditions: Admin logged in  
- Steps: Search account  
- Input: ACC001  
- Expected: Details shown  
- Actual: Same as expected  
- Status: Pass  

---

## TC039 - Export CSV
- Feature: Admin  
- Scenario: Export transactions  
- Preconditions: Admin logged in  
- Steps: Export  
- Input: N/A  
- Expected: CSV created  
- Actual: Same as expected  
- Status: Pass  

---

## TC040 - CSV With Data
- Feature: Export  
- Scenario: With transactions  
- Preconditions: Data exists  
- Steps: Export  
- Input: N/A  
- Expected: File created  
- Actual: Same as expected  
- Status: Pass  

---

## TC041 - CSV Empty Export
- Feature: Export  
- Scenario: No data  
- Preconditions: No transactions  
- Steps: Export  
- Input: N/A  
- Expected: No file or empty file  
- Actual: No transactions found  
- Status: Pass  

---

## TC042 - Security Lock
- Feature: Security  
- Scenario: Wrong PIN 3 times  
- Preconditions: Account exists  
- Steps: Enter wrong PIN  
- Input: Wrong PIN  
- Expected: Account locked  
- Actual: Account locked  
- Status: Pass  

---

## TC043 - PIN Validation
- Feature: Security  
- Scenario: Invalid PIN  
- Preconditions: System running  
- Steps: Enter PIN  
- Input: 12  
- Expected: 4 digits only  
- Actual: PIN must be exactly 4 digits  
- Status: Pass  

---

## TC044 - Account Validation
- Feature: Security  
- Scenario: Wrong account format  
- Preconditions: System running  
- Steps: Create account  
- Input: 12345  
- Expected: Must start ACC  
- Actual: Account Number must start with ACC  
- Status: Pass  

---

## TC045 - Invalid Menu Input
- Feature: Input  
- Scenario: Non-number menu  
- Preconditions: System running  
- Steps: Enter menu option  
- Input: abc  
- Expected: Invalid input  
- Actual: Invalid input  
- Status: Pass  

---

## TC046 - Blank Name (BUG FIX)
- Feature: Input Validation  
- Scenario: Empty name  
- Preconditions: System running  
- Steps: Register  
- Input: " "  
- Expected: Invalid Name  
- Actual: Invalid Name  
- Status: Pass  

---

## TC047 - Invalid Amount Input
- Feature: Input  
- Scenario: Text instead of number  
- Preconditions: User logged in  
- Steps: Enter amount  
- Input: hello  
- Expected: Invalid amount  
- Actual: Invalid amount  
- Status: Pass  
