# CheckPoint 1 - On-line Store (2TDSPF)

## Integrantes
Thiago Rodrigues da Mota, RM: 563650  
Moisés Waidemann Molinillo Júnior, RM: 563719  
Gabriel Sbrana Campos, RM: 565849  
___ 
## Domínio Escolhido
Este projeto consiste em uma loja on-line onde o cliente pode visualizar produtos, realizar pedidos, escolher a forma de pagamento e avaliar os itens adquiridos. 
___

## Entidades Modeladas
- **Costumer**: Recebe os seguintes atributos: IdPayment, IdAdress, Name, Birthdate, Gender e Email.
- **Payment**: Recebe os seguintes atributos: PaymentWay e Value.
- **Category**: Recebe os seguintes atributos: Name e Description.
- **Product**: Recebe os seguintes atributos: IdCategory, Name, Description, Price e Stock.
- **RatingProduct**: Recebe os seguintes atributos: IdCostumer, IdProduct e Score.
- **Address**: Recebe os seguintes atributos: IdCostumer, Street, City, State, PostalCode, Number e Country.
___

## Resumo dos relacionamentos
O relacionamento funciona da seguinte forma:

- **T_CP1_COSTUMER - 1:1 - T_CP1_ADDRESS:** Nessa ligação de uma compra on-line, um cliente pode ter um endereço para receber o produto, e o endereço sempre terá um cliente relacionado.



- **T_CP1_COSTUMER - N:1 - T_CP1_PAYMENT:** Nessa ligação de uma compra on-line, um cliente pode ter uma forma de pagamento, o pagamento sempre pode ter um ou mais clientes.



- **T_CP1_PRODUCT - 1:N - T_CP1_RATING_PRODUCT:** Nessa ligação de uma compra on-line, uma avaliação sempre terá um id de produto, porém um produto pode ter uma ou mais avaliações.



- **T_CP1_PRODUCT - N:1 - T_CP1_RATING_CATEGORY:** Nessa ligação de uma compra on-line, um produto sempre terá uma categoria, e uma categoria pode ter um ou nenhum produto relacionado.


- **T_CP1_COSTUMER - 1:N - T_CP1_RATING_PRODUCT:** Nessa ligação de uma compra on-line, uma avaliação sempre terá o id do cliente, e o cliente pode ter uma ou mais avaliações.