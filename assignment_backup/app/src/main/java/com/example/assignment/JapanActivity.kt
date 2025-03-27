package com.example.assignment

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.ImageButton

class JapanActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_japan)

        val imageButtonChawanmushi = findViewById<ImageButton>(R.id.imageButtonChawanmushi)
        val imageButtonWafu = findViewById<ImageButton>(R.id.imageButtonWafu)
        val imageButtonMiso = findViewById<ImageButton>(R.id.imageButtonMiso)
        val buttonChawanmushi = findViewById<Button>(R.id.buttonChawanmushi)
        val buttonWafu = findViewById<Button>(R.id.buttonWafu)
        val buttonMiso = findViewById<Button>(R.id.buttonMiso)

        imageButtonChawanmushi.setOnClickListener {
            val intent = Intent(this, ChawanmushiActivity::class.java)
            startActivity(intent)
        }

        imageButtonWafu.setOnClickListener {
            val intent = Intent(this, WafuActivity::class.java)
            startActivity(intent)
        }

        imageButtonMiso.setOnClickListener {
            val intent = Intent(this, MisoActivity::class.java)
            startActivity(intent)
        }

        buttonChawanmushi.setOnClickListener {
            val intent = Intent(this, ChawanmushiActivity::class.java)
            startActivity(intent)
        }

        buttonWafu.setOnClickListener {
            val intent = Intent(this, WafuActivity::class.java)
            startActivity(intent)
        }

        buttonMiso.setOnClickListener {
            val intent = Intent(this, MisoActivity::class.java)
            startActivity(intent)
        }
    }
}