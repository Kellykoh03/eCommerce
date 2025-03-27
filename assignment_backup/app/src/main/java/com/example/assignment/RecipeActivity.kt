package com.example.assignment

import android.content.Intent
import android.net.Uri
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.ImageButton

class RecipeActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_recipe)

        val buttonMalaysia = findViewById<ImageButton>(R.id.malaysiaButton)
        val buttonGermany = findViewById<ImageButton>(R.id.germanyButton)
        val buttonJapan = findViewById<ImageButton>(R.id.japanButton)
        val buttonMoreRecipe = findViewById<Button>(R.id.buttonMoreRecipe)

        buttonMalaysia.setOnClickListener {
            val intent = Intent(this, MalaysiaActivity::class.java)
            startActivity(intent)
        }

        buttonGermany.setOnClickListener {
            val intent = Intent(this, GermanyActivity::class.java)
            startActivity(intent)
        }

        buttonJapan.setOnClickListener {
            val intent = Intent(this, JapanActivity::class.java)
            startActivity(intent)
        }

        buttonMoreRecipe.setOnClickListener {
            val uri: String = "https://www.simplyrecipes.com/"

            val intent = Intent(Intent.ACTION_VIEW)
            if (intent.resolveActivity(packageManager) != null) {
                intent.data = Uri.parse(uri)
                startActivity(intent)
            }
        }

    }
}