BEGIN
INSERT INTO tblMovie (Id, Title, Description, Cost, RatingId, FormatId, DirectorId, InStkQty, ImagePath)
VALUES
    (1, 'Jurassic Park', 'Dinosaurs come to life', 14.99, 1, 2, 1, 100, 'jurassic_park.jpg'),
    (2, 'The Dark Knight', 'Batman fights the Joker', 12.99, 2, 2, 2, 150, 'dark-knight.jpg'),
    (3, 'Pulp Fiction', 'Crime and redemption', 9.99, 3, 1, 3, 75, 'pulp-fiction.jpeg');
END