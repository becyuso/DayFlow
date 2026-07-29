namespace DayFlow.Modules.Notes.Application.Messages
{
    public static class NoteMessageCodes
    {
        private const string NOTES = "NOTES";

        public const string NotebookNotFound =
            $"{NOTES}_NOTEBOOK_NOT_FOUND";

        public const string NotebookDeleted =
            $"{NOTES}_NOTEBOOK_DELETED";

        public const string NotebookAccessDenied =
            $"{NOTES}_NOTEBOOK_ACCESS_DENIED";

        public const string NotebookNoteLimitExceeded =
            $"{NOTES}_NOTEBOOK_NOTE_LIMIT_EXCEEDED";

        public const string NoteNotFound =
            $"{NOTES}_NOTE_NOT_FOUND";

        public const string NoteTitleTooLong =
            $"{NOTES}_NOTE_TITLE_TOO_LONG";

    }
}
