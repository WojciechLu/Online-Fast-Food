import styled from "styled-components";

export const DishContainer = styled.div`
&&&{
    width: 100%;
    background: green;
    overflow:hidden;
    display: grid;
    grid-template-columns: [line1] 10rem [line2] auto [line3] 10rem [end];
    grid-template-rows: [line1] 2.2rem [line2] 4.8rem [line3] 3rem [end];
    border-radius: 0.45rem;
    .dishName{
        grid-column-start: line2;
        grid-column-end: line3;
        grid-row-start: line1;
        grid-row-end: line2;
    }
    .dishImg{
        grid-column-start: line1;
        grid-column-end: line2;
        grid-row-start: line1;
        grid-row-end: end;

        padding: 0.2rem 0 0.5rem 0.2rem;
        max-width: 100%;
        min-height: 10rem;
    }
    .dishDescription{
        grid-column-start: line2;
        grid-column-end: end;
        grid-row-start: line2;
        grid-row-end: line3;

        overflow:auto;
        text-align: justify;
        margin: 0 1rem 0.5rem 1rem !important;
        padding-right: 0.4rem;
    }
    //custom scrollbar
    .dishDescription::-webkit-scrollbar {
        width: 0.4em;
    }
    .dishDescription::-webkit-scrollbar-track {
        -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);
    }
    .dishDescription::-webkit-scrollbar-thumb {
        background: green;
        background-clip: content-box;
        border: 5px solid;
    }

    button{
        grid-column-start: line3;
        grid-column-end: end;
        grid-row-start: line3;
        grid-row-end: end;
    }
}
`