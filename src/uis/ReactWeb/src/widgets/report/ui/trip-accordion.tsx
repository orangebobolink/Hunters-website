import AnimalPopularPie from '@/entities/report/ui/animal-popular-pie';
import {
    Accordion,
    AccordionContent,
    AccordionItem,
    AccordionTrigger,
} from '@/shared/ui/accordion';

const TripAccordion = () => {
    return (
        <Accordion type='single' collapsible className='w-full'>
            <AccordionItem value='item-1'>
                <AccordionTrigger>
                    Проведенные охоты по животным
                </AccordionTrigger>
                <AccordionContent>
                    <AnimalPopularPie />
                </AccordionContent>
            </AccordionItem>
        </Accordion>
    );
};

export default TripAccordion;
