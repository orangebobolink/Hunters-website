import {ThemeProvider} from '@/shared/lib/providers/theme-provider.tsx';
import {Router} from '@/app/router.tsx';
import {BrowserRouter} from 'react-router-dom';
import {ReduxProvider} from '@/app/providers/redux-provider.tsx';
import {AuthProvider} from '@/app/providers/auth-provider.tsx';
import TranslateProvider from '@/app/providers/translate-provider.tsx';
import SignalRProvider from '@/app/providers/signalR-provider.tsx';

function App() {
    return (
        <ReduxProvider>
            <AuthProvider>
                <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
                    <SignalRProvider>
                        <TranslateProvider>
                            <BrowserRouter>
                                <Router/>
                            </BrowserRouter>
                        </TranslateProvider>
                    </SignalRProvider>
                </ThemeProvider>
            </AuthProvider>
        </ReduxProvider>
    )
}

export default App
