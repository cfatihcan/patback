using System.Collections.Generic;
using System.Data.SqlClient;
using patback.Models;
using Dapper;
using System;
using Google.Cloud.Firestore;
using Newtonsoft.Json;

using patback.Utiltiy;
using System.Threading.Tasks;

namespace patback.Services
{
    public class PatientService
    {
        public static PatientService Instance = new PatientService();
        public async Task<IEnumerable<Patient>> GetAllAsync(int page, int pagesize)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConstFabric.dbPath))
                {
                    var patiList = await connection.QueryAsync<Patient>(" SELECT  * FROM (select ROW_NUMBER() OVER ( ORDER BY CREATE_DATETIME DESC) AS RowNum, * from PATIENT_CONTROL) " +
                      "AS result  WHERE   RowNum >= " + page + "AND RowNum <= " + pagesize + " ORDER BY RowNum"
                      );
                    return patiList;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private void addLog(Patient patient, int id)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "plogfire.json";
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
                FirestoreDb db = FirestoreDb.Create("plog-969c9");
                patient.createStamp = Timestamp.GetCurrentTimestamp();
                patient.ID = id;
                patient.logData = JsonConvert.SerializeObject(patient);
                DocumentReference docRef = db.Collection("log").Document();
                docRef.SetAsync(patient);
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public Returned createPatient(Patient patient)
        {
            Returned returnAnswer = new Returned();
            using (SqlConnection connection = new SqlConnection("Server=localhost;Database=PATIENTDB;Trusted_Connection=True;"))
            {
                try
                {
                    connection.Open();
                    string insertQuery = @"INSERT INTO [dbo].[PATIENT_CONTROL]([POLICLINIC_CODE], [DOCTOR_REC_ID], [DOCTOR_NAME_SURNAME], [PATIENT_NAME],
                 [PATIENT_SURNAME], [PATIENT_BIRTHDAY], [PATIENT_SEX], [PATIENT_REC_ID], [PATIENT_PHONE] ,[VISIT_DATETIME],[NEXT_VISIT_DATETIME],[DOCTOR_NOTE])  OUTPUT INSERTED.Id VALUES
                  (@POLICLINIC_CODE, 
                  @DOCTOR_REC_ID, 
                  @DOCTOR_NAME_SURNAME,
                  @PATIENT_NAME, 
                  @PATIENT_SURNAME, 
                  @PATIENT_BIRTHDAY,
                  @PATIENT_SEX,
                  @PATIENT_REC_ID,
                  @PATIENT_PHONE,
                  @VISIT_DATETIME,
                  @NEXT_VISIT_DATETIME,
                  @DOCTOR_NOTE
                )";
                    var resultid = connection.QuerySingle<int>(insertQuery, new
                    {
                        patient.POLICLINIC_CODE,
                        patient.DOCTOR_REC_ID,
                        patient.DOCTOR_NAME_SURNAME,
                        patient.PATIENT_NAME,
                        patient.PATIENT_SURNAME,
                        patient.PATIENT_BIRTHDAY,
                        patient.PATIENT_SEX,
                        patient.PATIENT_REC_ID,
                        patient.PATIENT_PHONE,
                        patient.VISIT_DATETIME,
                        patient.NEXT_VISIT_DATETIME,
                        patient.DOCTOR_NOTE

                    });
                    connection.Dispose();
                    returnAnswer.error = null;
                    returnAnswer.message = "Ok.";
                    returnAnswer.recId = resultid;

                    addLog(patient, resultid);

                }
                catch (System.Exception e)
                {

                    returnAnswer.error = e.Message;
                    returnAnswer.message = "Bad. Request";
                    returnAnswer.recId = 0;

                }

            }
            return returnAnswer;

        }

    }
}