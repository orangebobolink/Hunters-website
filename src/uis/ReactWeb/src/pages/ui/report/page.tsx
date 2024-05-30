import {
    Accordion,
    AccordionContent,
    AccordionItem,
    AccordionTrigger,
} from '@/shared/ui/accordion';
import RentAccordion from '@/widgets/report/ui/rent-accordion';
import TripAccordion from '@/widgets/report/ui/trip-accordion';

const ReportPage = () => {
    return (
        <div className='w-full flex flex-col flex-wrap justify-center items-center mt-10'>
            <Accordion type='single' collapsible className='w-3/5'>
                <AccordionItem value='item-1'>
                    <AccordionTrigger className='text-2xl'>
                        Аренда
                    </AccordionTrigger>
                    <AccordionContent className='mx-10'>
                        <RentAccordion />
                    </AccordionContent>
                </AccordionItem>
                <AccordionItem value='item-2'>
                    <AccordionTrigger className='text-2xl'>
                        Путевка
                    </AccordionTrigger>
                    <AccordionContent className='mx-10'>
                        <TripAccordion />
                    </AccordionContent>
                </AccordionItem>
            </Accordion>
        </div>
    );
};

export default ReportPage;
