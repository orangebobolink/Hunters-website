import {ThemeProvider} from '@/shared/lib/providers/theme-provider.tsx';
import {Router} from '@/app/router.tsx';
import {BrowserRouter} from 'react-router-dom';
import {ReduxProvider} from '@/app/providers/redux-provider.tsx';
import {AuthProvider} from '@/app/providers/auth-provider.tsx';
import TranslateProvider from '@/app/providers/translate-provider.tsx';
import SignalRProvider from '@/app/providers/signalR-provider.tsx';
import {Toaster} from '@/shared/ui/toaster.tsx';

function App() {
    return (
        <ReduxProvider>
            <AuthProvider>
                <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
                        <TranslateProvider>
                            <BrowserRouter>
                                <Router/>
                            </BrowserRouter>
                        </TranslateProvider>
                </ThemeProvider>
            </AuthProvider>
            <Toaster />
        </ReduxProvider>
    )
}

export default App

/*
 <SignalRProvider>
                        <TranslateProvider>
                            <BrowserRouter>
                                <Router/>
                            </BrowserRouter>
                        </TranslateProvider>
                    </SignalRProvider>
 */