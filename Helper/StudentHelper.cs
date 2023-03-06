using BootcampAPI.Input;
using BootcampAPI.Models;
using BootcampAPI.Output;
using System.Linq;
namespace BootcampAPI.Helper
{
    public class StudentHelper
    {
        private SchoolDBContext dBContext;
        public StudentHelper(SchoolDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public StatusOuput DeleteStudent(int id)
        {
            var returnValue = new StatusOuput();
            try
            {
                var student = dBContext.MsStudent.Where(x => x.id == id).FirstOrDefault();
                if (student == null)
                {
                    returnValue.statusCode = 404;
                    returnValue.message = "Student Not Found";
                    return returnValue;
                }
                dBContext.MsStudent.Remove(student);
                dBContext.SaveChanges();
                returnValue.statusCode = 200;
                returnValue.message = "Student Deleted";
                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public StatusOuput CreateStudent(StudentInput? data)
        {
            var returnValue = new StatusOuput();
            try
            {
                if (data != null)
                {
                    var gender = dBContext.MsGender.Where(x => x.id == data.gender).FirstOrDefault();
                    var religion = dBContext.MsReligion.Where(r => r.id == data.religion).FirstOrDefault();
                    if (gender == null)
                    {
                        returnValue.statusCode = 404;
                        returnValue.message = "gender not found";
                        return returnValue;
                    }
                    if (religion == null)
                    {
                        returnValue.statusCode = 404;
                        returnValue.message = "religion not found";
                        return returnValue;
                    }
                    if (data.name == null)
                    {
                        returnValue.statusCode = 204;
                        returnValue.message = "Name cannot be empty";
                        return returnValue;
                    }
                    if (data.email == null)
                    {
                        returnValue.statusCode = 204;
                        returnValue.message = "email cannot be empty";
                        return returnValue;
                    }
                    if (data.address == null)
                    {
                        returnValue.statusCode = 204;
                        returnValue.message = "address cannot be empty";
                        return returnValue;
                    }
                    if (data.BirthDate == DateTime.MinValue)
                    {
                        returnValue.statusCode = 204;
                        returnValue.message = "birthdate cannot be empty";
                        return returnValue;
                    }
                    var student = new MsStudent
                    {
                        name = data.name,
                        birthdate = data.BirthDate,
                        genderId = gender.id,
                        religionId = religion.id,
                        email = data.email,
                        address = data.address,
                    };
                    dBContext.MsStudent.Add(student);
                    dBContext.SaveChanges();

                    var subjects = dBContext.MsSubject.ToList();
                    foreach(var subject in subjects)
                    {
                        var score1 = new TrScore
                        {
                            studentId = student.id,
                            subjectId = subject.id,
                            semester = 1,
                            grade = "E",
                        };
                        var score2 = new TrScore
                        {
                            studentId = student.id,
                            subjectId = subject.id,
                            semester = 2,
                            grade = "E",
                        };
                        dBContext.TrScore.Add(score1);
                        dBContext.TrScore.Add(score2);
                        dBContext.SaveChanges();
                    }
                    returnValue.statusCode = 201;
                    returnValue.message = "student created";
                    return returnValue;
                }
                else
                {
                    returnValue.statusCode=204;
                    returnValue.message = "data cannot be empty";
                    return returnValue;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public StatusOuput UpdateStudent(StudentInput data)
        {
            var returnValue = new StatusOuput();
            try
            {
                var student = dBContext.MsStudent.Where(x => x.id == data.studentId).FirstOrDefault();
                if (student == null)
                {
                    returnValue.statusCode = 404;
                    returnValue.message = "student not found";
                    return returnValue;
                }
                if(data.BirthDate != DateTime.MinValue)
                {
                    student.birthdate = data.BirthDate;
                }
                if(data.gender != null)
                {
                    var gender = dBContext.MsGender.Where(x=> x.id == data.gender).FirstOrDefault();
                    if(gender != null)
                    {
                        student.genderId = gender.id;
                    }
                    else
                    {
                        returnValue.statusCode = 404;
                        returnValue.message = "gender not found";
                        return returnValue;
                    }
                }
                if (data.religion != null)
                {
                    var religion = dBContext.MsGender.Where(x => x.id == data.gender).FirstOrDefault();
                    if (religion != null)
                    {
                        student.genderId = religion.id;
                    }
                    else
                    {
                        returnValue.statusCode = 404;
                        returnValue.message = "religion not found";
                        return returnValue;
                    }
                    if(data.email != null)
                    {
                        student.email = data.email;
                    }
                    if (data.address != null)
                    {
                        student.address = data.address;
                    }
                    if(data.ScoreList != null)
                    {
                        foreach(var score in data.ScoreList)
                        {
                            var subject = dBContext.MsSubject.Where(x => x.id == score.subjectId).FirstOrDefault();
                            if (subject == null)
                            {
                                returnValue.statusCode = 404;
                                returnValue.message = "subject not found";
                                return returnValue;
                            }
                            else
                            {
                                if(score.semester1Score != null)
                                {
                                    var scoreData = dBContext.TrScore.Where(x => x.semester == 1 
                                    && x.subjectId == score.subjectId &&
                                    x.studentId == student.id).FirstOrDefault();
                                    if(scoreData != null)
                                    {
                                        scoreData.score = (decimal)score.semester1Score;
                                        var grade = dBContext.MsGrade.Where(x => score.semester1Score >= x.minScore &&
                                        score.semester1Score <= x.maxScore).FirstOrDefault();
                                        scoreData.grade = grade.name;
                                        dBContext.TrScore.Update(scoreData);
                                    }
                                }
                                if (score.semester2Score != null)
                                {
                                    var scoreData = dBContext.TrScore.Where(x => x.semester == 2
                                    && x.subjectId == score.subjectId &&
                                    x.studentId == student.id).FirstOrDefault();
                                    if (scoreData != null)
                                    {
                                        scoreData.score = (decimal)score.semester2Score;
                                        var grade = dBContext.MsGrade.Where(x => score.semester2Score >= x.minScore &&
                                        score.semester2Score <= x.maxScore).FirstOrDefault();
                                        scoreData.grade = grade.name;
                                        dBContext.TrScore.Update(scoreData);
                                    }
                                }
                            }
                        }
                    }
                }
                dBContext.MsStudent.Update(student);
                dBContext.SaveChanges();
                returnValue.statusCode = 200;
                returnValue.message = "student updated";
                return returnValue;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }

        }
        public studentData? GetStudentData(int id)
        {
            var returnValue = new studentData();
            try
            {
                var studentData = dBContext.MsStudent.Where(x => x.id == id).FirstOrDefault();
                if(studentData != null)
                {
                    var genderData = dBContext.MsGender.Where(x => x.id == studentData.genderId).FirstOrDefault();
                    var religionData = dBContext.MsReligion.Where(x => x.id == studentData.religionId).FirstOrDefault();
                    // join table
                    var scoreList1 = dBContext.TrScore.Where(x => x.studentId == studentData.id && x.semester == 1)
                        .Join(dBContext.MsSubject,
                        score => score.subjectId,
                        subject => subject.id,
                        (score, subject) => new
                        { 
                            score.studentId,
                            subjectId = subject.id,
                            subjectName = subject.name,
                            score.semester,
                            score.score,
                        }).ToList();
                    var scoreList2 = dBContext.TrScore.Where(x => x.studentId == studentData.id && x.semester == 2)
                        .Join(dBContext.MsSubject,
                        score => score.subjectId,
                        subject => subject.id,
                        (score, subject) => new
                        {
                            score.studentId,
                            subjectId = subject.id,
                            subjectName = subject.name,
                            score.semester,
                            score.score,
                        }).ToList();

                    var scoreList = scoreList1
                        .Join(scoreList2, 
                    scoreList1 => new { scoreList1.studentId, scoreList1.subjectId },
                    scoreList2 => new { scoreList2.studentId, scoreList2.subjectId },
                    (scoreList1, scoreList2) => new Score(){
                       subjectId = scoreList1.subjectId,
                       subjectName = scoreList1.subjectName,
                       semester1Score = scoreList1.score,
                       semester2Score = scoreList2.score,
                       finalScore = scoreList1.score + scoreList2.score / 2
                    }).ToList();
                    decimal totalScore = 0;
                    foreach(var score in scoreList)
                    {
                        totalScore += score.finalScore;
                    }

                    var averageFinalScore = totalScore / scoreList.Count;
                    var grade = dBContext.MsGrade.Where(x => averageFinalScore >= x.minScore 
                    && averageFinalScore <= x.maxScore).FirstOrDefault();
                    var student = new Student
                    {
                        id = studentData.id,
                        name = studentData.name,
                    };

                    var gender = new Gender
                    {
                        id = genderData.id,
                        description = genderData.description,
                    };
                    var religion = new religion
                    {
                        id = religionData.id,
                        description = religionData.description,
                    };
                    returnValue.student = student;
                    returnValue.birthdate = studentData.birthdate;
                    returnValue.gender = gender;
                    returnValue.religion = religion;
                    returnValue.email = studentData.email;
                    returnValue.averageFinalScore = Math.Round(averageFinalScore, 2);

                    return returnValue;

                }

                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
