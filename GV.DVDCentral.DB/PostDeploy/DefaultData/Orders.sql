BEGIN
INSERT INTO tblOrder (Id, CustomerId, OrderDate, UserId, ShipDate)
VALUES
    (1, 1, '2023-09-08', 1, '2023-09-10'),
    (2, 2, '2023-09-08', 2, '2023-09-11'),
    (3, 3, '2023-09-08', 3, '2023-09-12');
END