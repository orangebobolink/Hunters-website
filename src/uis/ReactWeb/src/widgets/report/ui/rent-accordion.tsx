import ProductPopularBar from '@/entities/report/ui/product-popular-bar';
import ProductRevenueBar from '@/entities/report/ui/product-revenue-bar';
import {
    Accordion,
    AccordionContent,
    AccordionItem,
    AccordionTrigger,
} from '@/shared/ui/accordion';

const RentAccordion = () => {
    return (
        <Accordion type='single' collapsible className='w-full'>
            <AccordionItem value='item-1'>
                <AccordionTrigger>
                    Сортировка продуктов по популярности
                </AccordionTrigger>
                <AccordionContent>
                    <ProductPopularBar />
                </AccordionContent>
            </AccordionItem>
            <AccordionItem value='item-2'>
                <AccordionTrigger>Доход от продуктов</AccordionTrigger>
                <AccordionContent>
                    <ProductRevenueBar />
                </AccordionContent>
            </AccordionItem>
        </Accordion>
    );
};

export default RentAccordion;
