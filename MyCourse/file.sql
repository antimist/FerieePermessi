info: Microsoft.EntityFrameworkCore.Infrastructure[10403]
      Entity Framework Core 3.0.3 initialized 'MyCourseDbContext' using provider 'Microsoft.EntityFrameworkCore.Sqlite' with options: MaxPoolSize=128 
CREATE UNIQUE INDEX "IX_Courses_Title" ON "Courses" ("Title");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220413124044_UniqueCourseTitle', '3.0.3');

CREATE TRIGGER CoursesSetRowVersionOnInsert
                                   AFTER INSERT ON Courses
                                   BEGIN
                                   UPDATE Courses SET RowVersion = CURRENT_TIMESTAMP WHERE Id=NEW.Id;
                                   END;

CREATE TRIGGER CoursesSetRowVersionOnUpdate
                                    AFTER UPDATE ON Courses WHEN NEW.RowVersion <= OLD.RowVersion
                                    BEGIN
                                        UPDATE Courses SET RowVersion = CURRENT_TIMESTAMP WHERE Id=NEW.Id;
                                    END;

UPDATE Courses SET RowVersion = CURRENT_TIMESTAMP;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220413124819_TriggersCurseVersion', '3.0.3');


