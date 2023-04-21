export const ENDPOINTS_MAP = {
    AUTHENTICATION: {
        GET_CURRENT_USER: 'account',
        LOGIN: 'account/login',
        REGISTER: 'account/register',
        GET_ALL_USERS: 'account/getAllUsers',
    },

    QUESTIONS: {
        GET_ALL_QUESTIONS: 'question/getAllQuestions',
        GET_QUESTION_DETAILS: 'question/getQuestionDetails/',
        POST_QUESTION: 'question/postQuestion',
        FILTER_QUESTIONS: 'question/filterQuestions/',
    },

    ANSWERS: {
        POST_ANSWER: 'answer/postAnswer',
        UPDATE_ANSWER: 'answer/updateAnswer',
        DELETE_ANSWER: 'answer/deleteAnswer/',
    },

    TAGS: {
        GET_ALL_TAGS: 'tag/getAllTags',
        POST_TAG: 'tag/createTag',
    },
};
