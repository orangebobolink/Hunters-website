import i18n from 'i18next';
import LanguageDetector from 'i18next-browser-languagedetector';
import Backend from 'i18next-http-backend';
import {initReactI18next} from 'react-i18next';
import translationRu from './ru/translation.json';
import translationBel from './bel/translation.json';

const resources = {
    ru: {
        translation: translationRu
    },
    bel:{
        translation: translationBel
    }
};

i18n
    .use(Backend)
    .use(LanguageDetector)
    .use(initReactI18next)
    .init({
        resources,
        debug: true,
        fallbackLng: 'ru',
    });

export {i18n}