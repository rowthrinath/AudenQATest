Feature: To test the feature to calculate short term loans
As a user applying for short term loans
I should be able to get quotes for monthly loans
    
Background:     
          Given I am able to launch credit short term loan portal
          
Scenario: To verify if user is able to select a loan amount and the amount is displayed correctly           
           When I select 210 loan amount using the slider
           Then I can see the selected amount in the loan section
           And I can see that the select slider amount matches with the amount in the loan section
           
Scenario: To verify the min and max ammounts of loan on slider          
           When I select 210 loan amount using the slider
           Then I can see the minimum amount to be 210
           When I select 500 loan amount using the slider
           Then I can see the minimum amount to be 500
           
Scenario: To verify that weekend cannot be a repayment day
          When I select 210 loan amount using the slider
          And I select a weekend as the repayment date 9
          Then I shouild see the first repayment day option be pushed back to the last working day