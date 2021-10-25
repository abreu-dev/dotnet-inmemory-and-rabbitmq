import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

import { BsModalService, ModalModule } from "ngx-bootstrap/modal";
import { NgxMaskModule } from "ngx-mask";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { CoreModule } from "./core/core.module";
import { VeiculoService } from "./features/veiculo/shared/services/veiculo.service";

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CoreModule,
    FormsModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
    NgxMaskModule.forRoot({
      dropSpecialCharacters: true,
    }),
  ],
  providers: [BsModalService, VeiculoService],
  bootstrap: [AppComponent],
})
export class AppModule {}
