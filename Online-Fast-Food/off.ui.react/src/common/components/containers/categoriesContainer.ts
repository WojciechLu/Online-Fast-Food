import styled from "styled-components";

export const CategoriesContainer = styled.div`
&&&{
    //overflow: auto;
    display: flex;
    flex-wrap: wrap;
    align-items: center;
    justify-content: center;
    gap: 0.1rem;
    div{
        min-height: 1.3rem;
        //colors
        background-color: pink;
        border: 0.2rem solid violet;
        border-radius: 0.45rem;
        padding: 0 0.6rem;
    }
    margin:0.5rem;
}
`