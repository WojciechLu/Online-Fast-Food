import styled from "styled-components";

export const DishContainer = styled.div`
&&&{
    width: 100%;
    overflow:hidden;
    background: green;
    position: relative;
    border-radius: 0.45rem;
    img{
        float: left;
        margin: 0 1rem 1rem 1rem;
        max-width: 100%;
        height: 165px;
    }
    p{
        text-align: justify;
        margin-left: 2rem;
        margin-right: 2rem;
    }
}
`