import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Editor, schema, toDoc, Toolbar } from 'ngx-editor';
import { Profile } from 'src/app/_models/Profile';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  profileForm: FormGroup;
  selectedFile: any = null;
  imageSrc;

  editor: Editor;
  toolbar: Toolbar = [
    ['bold', 'italic'],
    ['underline', 'strike'],
    ['code', 'blockquote'],
    ['ordered_list', 'bullet_list'],
    [{ heading: ['h1', 'h2', 'h3', 'h4', 'h5', 'h6'] }],
    ['link', 'image'],
    ['text_color', 'background_color'],
    ['align_left', 'align_center', 'align_right', 'align_justify'],
  ];

  constructor(
    private accountService: AccountService,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.editor = new Editor({ schema: schema });

    this.getProfile();
  }

  private getProfile() {
    this.accountService
      .getProfile(this.accountService.currentUserSource.value.id)
      .subscribe((data) => {
        this.imageSrc =
          data?.profilePicture ?? '../../../assets/empty-profile-pic.png';

        this.profileForm = this.formBuilder.group({
          description: [
            JSON.parse(data?.description ?? JSON.stringify(toDoc(''))),
          ],
        });
      });
  }

  public updateProfile() {
    const profile: Profile = {
      profilePicture: this.imageSrc,
      ...this.profileForm.value,
    };
    profile.description = JSON.stringify(profile.description);

    this.accountService
      .updateProfile(this.accountService.currentUserSource.value.id, profile)
      .subscribe({
        next: () => {
          this.snackBar.open('Successfully updated the profile!', 'Dismiss', {
            duration: 5000,
          });
        },
        error: () => {
          this.snackBar.open('Something went wrong', 'Dismiss', {
            duration: 5000,
          });
        },
      });
  }

  async onFileSelected(event: any) {
    this.selectedFile = event.target.files[0] ?? null;
    const base64 = await this.convertBase64(this.selectedFile);
    this.imageSrc = base64;
  }

  convertBase64 = (file) => {
    return new Promise((resolve, reject) => {
      const fileReader = new FileReader();
      fileReader.readAsDataURL(file);

      fileReader.onload = () => {
        resolve(fileReader.result);
      };

      fileReader.onerror = (error) => {
        reject(error);
      };
    });
  };
}
