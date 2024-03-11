BEGIN

INSERT INTO tblCustomer(Id, FirstName, LastName, Address, City, State, ZIP, Phone, UserId)
VALUES
    (1, 'John', 'Doe', '123 Main St', 'Los Angeles', 'CA', '90001', '9201234567', 1),
    (2, 'Jane', 'Smith', '456 Elm St', 'New York', 'NY', '10001', '5559876543', 2),
    (3, 'Bob', 'Johnson', '789 Oak St', 'Chicago', 'IL', '60601', '5555678901', 3);


END