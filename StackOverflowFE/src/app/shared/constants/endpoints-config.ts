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
    },

    ANSWERS: {
        POST_ANSWER: 'answer/postAnswer',
        UPDATE_ANSWER: 'answer/updateAnswer',
        DELETE_ANSWER: 'answer/deleteAnswer/',
    },
};
