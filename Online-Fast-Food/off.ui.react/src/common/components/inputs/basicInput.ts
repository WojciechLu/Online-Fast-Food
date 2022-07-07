import styled from "styled-components";

export const BasicInput = styled.input`
   &&&{
        background: white !important; 
        width:20em;
        border-radius:10rem;
        color:black;  
        height:1.5rem;
        margin:0.5rem 0;
        padding:0.5rem 0.4rem; 
        border:1px gray solid;
        
   }
   :focus-visible{
       outline:0px;
       background: white !important; 
   }
`;