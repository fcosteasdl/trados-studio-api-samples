﻿// ---------------------------------
// <copyright file="TMCreator.cs" company="SDL International">
// Copyright  2010 All Right Reserved
// </copyright>
// <author>Patrik Mazanek</author>
// <email>pmazanek@sdl.com</email>
// <date>2010-09-27</date>
// ---------------------------------
namespace Sdl.SDK.LanguagePlatform.Samples.BatchImporter
{
    using System.Globalization;
    using Sdl.LanguagePlatform.Core.Tokenization;
    using Sdl.LanguagePlatform.TranslationMemory;
    using Sdl.LanguagePlatform.TranslationMemoryApi;

    /// <summary>
    /// Represents functionality for creating Translation Memories.
    /// </summary>
    public class TMCreator
    {
        #region "createTM"

        /// <summary>
        /// Create the master TM based on the source and target locales found in the current TMX file.
        /// </summary>
        /// <param name="sourceLanguage">String representation of translation memory source language.</param>
        /// <param name="targetLanguage">String representation of translation memory target language.</param>
        /// <param name="masterPath">Path to translation memory.</param>
        public void CreateMasterTm(string sourceLanguage, string targetLanguage, string masterPath)
        {
            #region "Create Translation Memory"

            string path = string.Format(
                CultureInfo.CurrentCulture,
                "{0}MasterTm_{1}_{2}.sdltm",
                masterPath,
                sourceLanguage,
                targetLanguage);
            FileBasedTranslationMemory translationMemory = new FileBasedTranslationMemory(
                path,
                "Master TM",
                CultureInfo.GetCultureInfo(sourceLanguage),
                CultureInfo.GetCultureInfo(targetLanguage),
                this.GetFuzzyIndexes(),
                this.GetRecognizers(),
                TokenizerFlags.AllFlags,
                WordCountFlags.AllFlags
                );

            #endregion

            #region "Save"

            translationMemory.LanguageResourceBundles.Clear();
            translationMemory.Save();

            #endregion
        }
        #endregion

        #region "Get fuzzy indexes"

        /// <summary>
        /// Configure the fuzzy indexes, to determine, for example,
        /// that the master TM should support concordance searching 
        /// in the target language.
        /// The fuzzy indexes for source and target should be word-based only
        /// i.e. not character-based for performance reasons. (Master TMs
        /// can be assumed to be big, and therefore slow on character-based searches.)
        /// </summary>
        /// <returns>Hard-coded SourceWordBased and TargetWordBased FuzzyIndexes type</returns>
        private FuzzyIndexes GetFuzzyIndexes()
        {
            return FuzzyIndexes.SourceWordBased | FuzzyIndexes.TargetWordBased;
        }

        #endregion

        #region "get recognizers"

        /// <summary>
        /// Configure the recognition settings for the TM.
        /// Here, we simply activate all recognition settings 
        /// through RecognizeAll, which includes recognition
        /// of variables, dates, numbers, acronyms, measurements, and times.
        /// </summary>
        /// <returns>Hard-coded RecognizeAll BuiltinRecognizer.</returns>
        private BuiltinRecognizers GetRecognizers()
        {
            return BuiltinRecognizers.RecognizeAll;
        }

        #endregion
    }
}

