USE gregslisttimz;

CREATE TABLE profiles
(
  id VARCHAR(255) NOT NULL,
  email VARCHAR(255) NOT NULL,
  name VARCHAR(255),
  picture VARCHAR(255),
  PRIMARY KEY (id)
);

CREATE TABLE restaurants
(
  id INT NOT NULL AUTO_INCREMENT,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL UNIQUE,
  location VARCHAR(255) NOT NULL,
  owner VARCHAR(255) NOT NULL,
  PRIMARY KEY (id),

  -- REVIEW[epic=Populate] Establishing a relationship to a foreign key
  FOREIGN KEY (creatorId)
   REFERENCES profiles (id)
   ON DELETE CASCADE
);

CREATE TABLE reviews
(
   id INT NOT NULL AUTO_INCREMENT,
   memberId VARCHAR(255) NOT NULL,
   restaurantId INT NOT NULL,

  PRIMARY KEY (id),

  -- REVIEW[epic=many-to-many] Establishing a relationship to a foreign key
  FOREIGN KEY (memberId)
   REFERENCES profiles (id)
   ON DELETE CASCADE,

  -- REVIEW[epic=many-to-many] Establishing a relationship to a foreign key
  FOREIGN KEY (restaurantId)
   REFERENCES restaurants (id)
   ON DELETE CASCADE
);