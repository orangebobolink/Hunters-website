import {I18nextProvider} from 'react-i18next';
import {i18n} from '@/shared/config/i18n';
import {ReactNode} from 'react';

const TranslateProvider = ({ children }: { children: ReactNode }) => {
    return (
        <I18nextProvider i18n={i18n} defaultNS={'translation'}>
            {children}
        </I18nextProvider>
    );
};

export default TranslateProvider;