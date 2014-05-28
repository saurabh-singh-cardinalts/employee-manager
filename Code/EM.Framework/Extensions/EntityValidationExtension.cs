#region using

using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;

#endregion

namespace EM.Framework.Extensions
{
    public static class EntityValidationExtension
    {
        public static bool IsEntityException(this Exception exception)
        {
            return exception is DbEntityValidationException || exception is DbUpdateConcurrencyException ||
                   exception is DbUpdateException;
        }

        public static List<string> EntityException(this Exception exception)
        {
            var erros = new List<string>();
            try
            {
                if (exception is DbEntityValidationException)
                {
                    var e = exception as DbEntityValidationException;
                    foreach (var entityValidationError in e.EntityValidationErrors)
                    {
                        erros.Add(string.Format("Entity \"{0}\" in state \"{1}\", errors:",
                                                entityValidationError.Entry.Entity.GetType().Name,
                                                entityValidationError.Entry.State));

                        erros.AddRange(
                            entityValidationError.ValidationErrors.Select(
                                error =>
                                (string.Format(" (Property: \"{0}\", Error: \"{1}\")", error.PropertyName,
                                               error.ErrorMessage))));
                    }
                }
                else if (exception is DbUpdateConcurrencyException)
                {
                    var ex = exception as DbUpdateConcurrencyException;
                    var entry = ex.Entries.Single();
                    var clientValues = entry.CurrentValues.Clone().ToObject();
                    entry.Reload();
                    var databaseValues = entry.CurrentValues.ToObject();
                    var entityFromDbProperties =
                        databaseValues.GetType()
                                      .GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public |
                                                     BindingFlags.Instance);
                    var propertyNames =
                        databaseValues.GetType().GetProperties(BindingFlags.Public).Select(o => o.Name).ToList();
                    //Get all public properties of the entity that have names matching those in our modelstate.
                    foreach (var propertyInfo in entityFromDbProperties)
                    {
                        //Compare db value to the current value from the entity we posted.

                        if (propertyNames.Contains(propertyInfo.Name))
                        {
                            if (propertyInfo.GetValue(databaseValues, null) != propertyInfo.GetValue(clientValues, null))
                            {
                                var currentValue = propertyInfo.GetValue(databaseValues, null);
                                if (currentValue == null || string.IsNullOrEmpty(currentValue.ToString()))
                                {
                                    currentValue = "Empty";
                                }

                                erros.Add(string.Format("(Property: \"{0}\", Current value: : \"{1}\")",
                                                        propertyInfo.Name, currentValue));
                            }
                        }
                    }
                }
                else if (exception is DbUpdateException)
                {
                    var updateException = exception as DbUpdateException;

                    //often in innerException
                    erros.Add(string.Format("Detailed Exception: {0}\r\n", updateException.InnerException != null
                                                                               ? updateException.InnerException.ToString
                                                                                     ()
                                                                               : updateException.ToString()));
                    erros.Add("-----------------------------------------------------------------\r\n ");
                    erros.Add("Entities that could not be saved to Database are\r\n ");


                    erros.AddRange(updateException.Entries.Select(
                        entry =>
                        string.Format("Entity {0} with Keys {1}\r\n ", entry.Entity.GetType().Name,
                                      entry.CurrentValues.ToObject().ToString())));
                    erros.Add("------------------------------------------------------------------\r\n ");
                }
            }
            catch (Exception ex)
            {
                erros.Add("Problem in reading entity exception details" + ex.Message);
            }
            return erros;
        }
    }
}