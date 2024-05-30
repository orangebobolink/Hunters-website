import {Card, CardContent, CardHeader, CardTitle} from '@/shared/ui/card.tsx';
import Img from '@/assets/ai-img.jpg';

export const MainForm = () => {
    return (
        <div className="flex flex-col">
            <img alt="Image" src={Img} className="w-screen mt-[2dvh] h-[50lvh] object-cover bottom-0 left-0 -z-10"/>
            <div className="p-8 text-center">
                <Card>
                    <CardHeader>
                        <CardTitle>
                            Добро пожаловать на сайт охотников
                        </CardTitle>
                    </CardHeader>
                    <CardContent>
                        <p>
                            На нашем сайте вы найдете всю необходимую информацию для успешной
                            охоты.
                        </p>
                    </CardContent>
                </Card>
                <Card className="mt-4">
                    <CardHeader>
                        <CardTitle>Планирование Охоты</CardTitle>
                    </CardHeader>
                    <CardContent>
                        <p>
                            Ознакомьтесь с планами охоты, выберите подходящий и забронируйте
                            свою путевку онлайн.
                        </p>
                    </CardContent>
                </Card>
                <Card className="mt-4">
                    <CardHeader>
                        <CardTitle>Экипировка</CardTitle>
                    </CardHeader>
                    <CardContent>
                        <p>
                            У нас вы можете приобрести всю необходимую экипировку и аксессуары
                            для охоты.
                        </p>
                    </CardContent>
                </Card>
            </div>
        </div>
    );
}